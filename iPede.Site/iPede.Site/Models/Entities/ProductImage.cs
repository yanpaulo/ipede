using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    public class ProductImage
    {
        /// <summary>
        /// Path for the default images directory.
        /// </summary>
        public const string DefaultImageDirectory = "~/product-images/full/";
        /// <summary>
        /// Path for te default thumbnails directory
        /// </summary>
        public const string DefaultThumbDirectory = "~/product-images/thumb/";

        /// <summary>
        /// Path for the image represented by ProductImage.NoImage
        /// </summary>
        public const string NoImageFilePath = "~/Content/images/no-image.jpg";

        private static ProductImage noImage;

        /// <summary>
        /// Returns the static NoImage object.
        /// </summary>
        public static ProductImage NoImage
        {
            get
            {
                if (noImage == null)
                {
                    noImage = new ProductImage() { Filename = NoImageFilePath, ThumbFilename = NoImageFilePath, CustomDirectory = true };
                }
                return noImage;
            }
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        /// <summary>
        /// Tells whether the Filename/ThumbFilename properties contains a custom file path.
        /// </summary>
        public bool CustomDirectory { get; set; }
        
        /// <summary>
        /// Contains the name or a custom path for the image file.
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// Contains the name or a custom path for the thumbnail image file.
        /// </summary>
        public string ThumbFilename { get; set; }
        
        /// <summary>
        /// Gets the relative or absolute Url for the image file (which of those is more apropriate).
        /// </summary>
        [NotMapped]
        public string Url 
        {
            get
            {
                if (CustomDirectory)
                {
                    return Filename;
                }
                else
                {
                    return DefaultImageDirectory + Filename;
                }
            }
        }

        /// <summary>
        /// Gets the relative or absolute Url for the thumbnail image file (which of those is more apropriate).
        /// </summary>
        [NotMapped]
        public string ThumbUrl
        { 
            get
            {
                if (CustomDirectory)
                {
                    return ThumbFilename;
                }
                else
                {
                    return DefaultThumbDirectory + ThumbFilename;
                }
            }
        }

    }
}