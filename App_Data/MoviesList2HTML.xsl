<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

<xsl:template match="/">
  <html>
  <body>
  <h2>My Movies Collection</h2>
  <table border="1" style="border-collapse:collapse">
    <tr bgcolor="silver">
      <th>ID</th>
      <th>Title</th>
      <th>Director</th>
      <th>Release Date</th>
    </tr>
    <xsl:apply-templates select="moviesList/movie" />
  </table>
  </body>
  </html>
</xsl:template>

<xsl:template match="moviesList/movie">
    <tr>
      <td><xsl:value-of select="id"/></td>
      <td><xsl:value-of select="title"/></td>
      <td><xsl:value-of select="director"/></td>
      <td><xsl:value-of select="releaseDate"/></td>
    </tr>
</xsl:template>

</xsl:stylesheet>