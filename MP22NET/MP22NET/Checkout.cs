//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MP22NET
{
    using System;
    using System.Collections.Generic;
    
    public partial class Checkout
    {
        public int Id { get; set; }
        public int Benefit { get; set; }
        public string Name { get; set; }
        public int Carts { get; set; }
        public int c_left { get; set; }
        public int c_top { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
