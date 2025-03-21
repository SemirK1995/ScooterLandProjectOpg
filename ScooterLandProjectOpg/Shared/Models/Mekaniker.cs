﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterLandProjectOpg.Shared.Models
{
	public class Mekaniker
	{
		// Primary Key
		[Key]
		public int MekanikerId { get; set; }

		//Attributter
		public string? Navn { get; set; }
		public int? Telefonnummer { get; set; }
		public string? Speciale { get; set; }

		public Mekaniker()
		{
		}
	}
}
