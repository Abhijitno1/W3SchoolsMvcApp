using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using W3SchoolsMvcApp.Models;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace W3SchoolsMvcApp.Services
{
    public class PdfExporter : W3SchoolsMvcApp.Services.IExporter
    {
        public Byte[] ExportMoviesList(string viewContents)
        {
            var reader = new StringReader(viewContents);
            return ConvertHTML2PDF(reader);
        }

        public Byte[] ExportMoviesList(IEnumerable<Movie> moviesList)
        {
            string dataFilePath = Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory").ToString(), "CDCatalogSample.htm");
            var reader = new StreamReader(dataFilePath);
            return ConvertHTML2PDF(reader);
        }

        private Byte[] ConvertHTML2PDF(TextReader reader)
        {
            using (MemoryStream output = new MemoryStream())
            {
                using (Document doc = new Document(PageSize.A4, 30, 30, 30, 30))
                {                    
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, output))
                    {                       
                        writer.ViewerPreferences = PdfWriter.PageModeUseOutlines;
                        writer.PageEvent = new TwoColumnPdfHeaderFooter();
                        doc.Open();
                        //doc.NewPage();
                        using (HTMLWorker parser = new HTMLWorker(doc))
                            parser.Parse(reader);
                        doc.Close();
                    }
                }
                return output.ToArray();
            }
        }
    }

    public class TwoColumnPdfHeaderFooter : PdfPageEventHelper
    {
        PdfContentByte cb;
        BaseFont bf;
        PdfTemplate template;

        public DateTime PrintDateTime { get; private set; }
        public string Title { get; set; }
        public string HeaderLeft { get; set; }
        public string HeaderRight { get; set; }
        public Font HeaderFont { get; set; }
        public Font FooterFont { get; set; }

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                base.OnOpenDocument(writer, document);
                cb = writer.DirectContent;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                template = cb.CreateTemplate(50, 50);
                PrintDateTime = DateTime.Now;
            }
            catch (DocumentException ex)
            {
                throw;
            }
            catch (IOException ex)
            {
                throw;
            }
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {
            base.OnStartPage(writer, document);
            Rectangle pageSize = document.PageSize;

            if (!string.IsNullOrWhiteSpace(Title))
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 15);
                cb.SetRGBColorFill(50, 50, 200);
                cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetTop(40));
                cb.ShowText(Title);
                cb.EndText();
            }

            if (!string.IsNullOrWhiteSpace(HeaderLeft + HeaderRight))
            {
                PdfPTable headerTable = new PdfPTable(2);
                headerTable.TotalWidth = pageSize.Width - 80;
                headerTable.SetWidthPercentage(new float[] { 45, 45 }, pageSize);
                headerTable.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

                PdfPCell headerLeftCell = new PdfPCell(new Phrase(8, HeaderLeft, HeaderFont));
                headerLeftCell.Padding = 5;
                headerLeftCell.PaddingBottom = 8;
                headerLeftCell.BorderWidthRight = 0;
                headerTable.AddCell(headerLeftCell);

                PdfPCell headerRightCell = new PdfPCell(new Phrase(8, HeaderRight, HeaderFont));
                headerRightCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerRightCell.Padding = 5;
                headerRightCell.PaddingBottom = 8;
                headerRightCell.BorderWidthLeft = 0;
                headerTable.AddCell(headerRightCell);

                cb.SetRGBColorFill(0, 0, 0);
                headerTable.WriteSelectedRows(0, -1, pageSize.GetLeft(40), pageSize.GetTop(50), cb);
            }
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            Rectangle pageSize = document.PageSize;
            string pageText = "Page " + document.PageNumber;
            float pageTextWidth = bf.GetWidthPoint(pageText, 8);
            cb.SetRGBColorFill(100, 100, 100);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(40), pageSize.GetBottom(30));
            cb.ShowText(pageText);
            cb.EndText();

            cb.AddTemplate(template, pageSize.GetLeft(40) + pageTextWidth, pageSize.GetBottom(30));
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(Element.ALIGN_RIGHT, "Printed on " + PrintDateTime.ToShortDateString()
                , pageSize.GetRight(40), pageSize.GetBottom(30), 0);
            cb.EndText();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(0, 0);
            cb.ShowText(string.Empty + (writer.PageNumber - 1));
            cb.EndText();
        }
    }
}