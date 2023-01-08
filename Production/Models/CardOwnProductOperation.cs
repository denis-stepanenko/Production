﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Production.Models
{
    [Table("CRCardOwnProductOperations")]
    public class CardOwnProductOperation
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public decimal Labor { get; set; }
        public int Type { get; set; }
        public DateTime? Date { get; set; }
        public int Department { get; set; }

        public int? ExecutorId { get; set; }
        public Executor? Executor { get; set; }

        public int? ConfirmUserId { get; set; }
        public string? ConfirmUserName { get; set; }

        //public bool IsConfirmed => ConfirmUserId != null;

        public int CardOwnProductId { get; set; }
        public CardOwnProduct CardOwnProduct { get; set; }
    }
}
