using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iPede.Site.Models.Entities
{
    /// <summary>
    /// This Class represents the page 'Quem Somos'.
    /// 
    /// Each property represents some area of the page.
    /// The areas are divided acording to this, on the view:
    /// 
    ///      Quem Somos
    ///      +----------------+----------------+
    ///      | FistQuadrant   | SecondQuadrant |
    ///      +----------------+----------------+
    ///      | FourthQuadrant | ThirdQuadrant  |
    ///      +----------------+----------------+
    /// 
    /// </summary>
    public class AboutUs
    {

        [DefaultValue(1)]
        public int AboutUsId { get; set; }

        [DefaultValue("Sobre a Isofrio")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Sobre a Isofrio")]
        [Display(Name = "Título", Description = "Título da área superior esquerda da tela.")]
        public string FirstQuadrantTitle { get; set; }

        [DefaultValue("Descrição")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Descrição")]
        [Display(Name = "Descrição", Description = "Descrição da área superior esquerda da tela.")]
        public string FirstQuadrantDescription { get; set; }


        [DefaultValue("Monte Conosco")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Monte Conosco")]
        [Display(Name = "Título", Description = "Título da área superior direita da tela.")]
        public string SecondQuadrantTitle { get; set; }

        /// <summary>
        /// List that shall appear at the second quadrant. See the 'Quem somos' view.
        /// </summary>
        public virtual ICollection<String> SecondQuadrantList { get; set; }


        [DefaultValue("Nossos Serviços")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Nossos Serviços")]
        [Display(Name = "Nossos Serviços", Description = "Título da área inferior direita da tela.")]
        public string ThirdQuadrantTitle { get; set; }

        /// <summary>
        /// List that shall appear at the third quadrant. See the 'Quem somos' view.
        /// </summary>
        public virtual ICollection<String> ThirdQuadrantList { get; set; }


        [DefaultValue("Por que nos escolher?")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Por que nos escolher?")]
        [Display(Name = "Título", Description = "Título da área inferior esquerda da tela.")]
        public string FourthQuadrantTitle { get; set; }

        [DefaultValue("Descrição")]
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Descrição")]
        [Display(Name = "Descrição", Description = "Descrição da área inferior esquerda da tela.")]
        public string FourthQuadrantDescription { get; set; }

    }
}