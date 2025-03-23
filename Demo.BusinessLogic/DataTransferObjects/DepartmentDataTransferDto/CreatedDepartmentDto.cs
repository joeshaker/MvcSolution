﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DataTransferObjects.DepartmentDataTransferDto
{
    public class CreatedDepartmentDto
    {
        [Required(ErrorMessage = "Name is required!!!1")]
        public string Name { get; set; } = null!;
        [Required]
        [Range(100, int.MaxValue)]
        public string Code { get; set; } = null!;

        public string Description { get; set; }

        public DateOnly DateOfCreation { get; set; }
    }
}
