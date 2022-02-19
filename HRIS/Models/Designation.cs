﻿using System;
using System.ComponentModel.DataAnnotations;

namespace HRIS.Models
{
	public class Designation
	{
		public Guid Id { get; set; }
		[Required]
		public string Name { get; set; }
		public bool Closed { get; set; }
		public string Notes { get; set; }
	}
}
