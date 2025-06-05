using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquiposORM.Dominio
{
    public class OTM
    {
        [Key]
        public int Id { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public string Descripcion { get; set; } = string.Empty;
        public ICollection<OTMTopico> OTMTopicos { get; set; } = new List<OTMTopico>();
    }

    public class OTMTopico
    {
        [Key]
        public int OTMId { get; set; }
        public OTM OTM { get; set; }
        public int TopicoId { get; set; }
        public Topico Topico { get; set; }
    }
}
