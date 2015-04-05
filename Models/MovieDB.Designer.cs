﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
namespace W3SchoolsMvcApp.Models
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class MovieDBEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new MovieDBEntities object using the connection string found in the 'MovieDBEntities' section of the application configuration file.
        /// </summary>
        public MovieDBEntities() : base("name=MovieDBEntities", "MovieDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MovieDBEntities object.
        /// </summary>
        public MovieDBEntities(string connectionString) : base(connectionString, "MovieDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new MovieDBEntities object.
        /// </summary>
        public MovieDBEntities(EntityConnection connection) : base(connection, "MovieDBEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Movie> Movies
        {
            get
            {
                if ((_Movies == null))
                {
                    _Movies = base.CreateObjectSet<Movie>("Movies");
                }
                return _Movies;
            }
        }
        private ObjectSet<Movie> _Movies;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<EdmMetadata> EdmMetadatas
        {
            get
            {
                if ((_EdmMetadatas == null))
                {
                    _EdmMetadatas = base.CreateObjectSet<EdmMetadata>("EdmMetadatas");
                }
                return _EdmMetadatas;
            }
        }
        private ObjectSet<EdmMetadata> _EdmMetadatas;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Movies EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMovies(Movie movie)
        {
            base.AddObject("Movies", movie);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the EdmMetadatas EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToEdmMetadatas(EdmMetadata edmMetadata)
        {
            base.AddObject("EdmMetadatas", edmMetadata);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MovieDBModel", Name="EdmMetadata")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class EdmMetadata : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new EdmMetadata object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static EdmMetadata CreateEdmMetadata(global::System.Int32 id)
        {
            EdmMetadata edmMetadata = new EdmMetadata();
            edmMetadata.Id = id;
            return edmMetadata;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String ModelHash
        {
            get
            {
                return _ModelHash;
            }
            set
            {
                OnModelHashChanging(value);
                ReportPropertyChanging("ModelHash");
                _ModelHash = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ModelHash");
                OnModelHashChanged();
            }
        }
        private global::System.String _ModelHash;
        partial void OnModelHashChanging(global::System.String value);
        partial void OnModelHashChanged();

        #endregion

    
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="MovieDBModel", Name="Movie")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Movie : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Movie object.
        /// </summary>
        /// <param name="id">Initial value of the ID property.</param>
        /// <param name="title">Initial value of the Title property.</param>
        public static Movie CreateMovie(global::System.Int32 id, global::System.String title)
        {
            Movie movie = new Movie();
            movie.ID = id;
            movie.Title = title;
            return movie;
        }

        #endregion

        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 ID
        {
            get
            {
                return _ID;
            }
            set
            {
                if (_ID != value)
                {
                    OnIDChanging(value);
                    ReportPropertyChanging("ID");
                    _ID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("ID");
                    OnIDChanged();
                }
            }
        }
        private global::System.Int32 _ID;
        partial void OnIDChanging(global::System.Int32 value);
        partial void OnIDChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                OnTitleChanging(value);
                ReportPropertyChanging("Title");
                _Title = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Title");
                OnTitleChanged();
            }
        }
        private global::System.String _Title;
        partial void OnTitleChanging(global::System.String value);
        partial void OnTitleChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Director
        {
            get
            {
                return _Director;
            }
            set
            {
                OnDirectorChanging(value);
                ReportPropertyChanging("Director");
                _Director = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Director");
                OnDirectorChanged();
            }
        }
        private global::System.String _Director;
        partial void OnDirectorChanging(global::System.String value);
        partial void OnDirectorChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> ReleaseDate
        {
            get
            {
                return _ReleaseDate;
            }
            set
            {
                OnReleaseDateChanging(value);
                ReportPropertyChanging("ReleaseDate");
                _ReleaseDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ReleaseDate");
                OnReleaseDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _ReleaseDate;
        partial void OnReleaseDateChanging(Nullable<global::System.DateTime> value);
        partial void OnReleaseDateChanged();

        #endregion

    
    }

    #endregion

    
}
