using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AE.CustomerApp.Domain.Models
{
    public abstract class BaseDomainEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Required]
        public virtual DateTime CreatedDate { get; set; }

        [Required]
        public virtual DateTime UpdatedDate { get; set; }

        public virtual void SetCreatedDate()
        {
            CreatedDate = DateTime.Now;
        }

        public virtual void SetUpdatedDate()
        {
            UpdatedDate = DateTime.Now;
        }
    }
}
