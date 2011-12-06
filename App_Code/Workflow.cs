/*
 * Copyright 2011 
 * Ajay Mandvekar(ajaymandvekar@gmail.com),Mugdha Kolhatkar(himugdha@gmail.com),Vishakha Channapattan(vishakha.vc@gmail.com)
 * 
 * This file is part of TenantCustomizationApp.
 * TenantCustomizationApp is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * TenantCustomizationApp is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with TenantCustomizationApp.  If not, see <http://www.gnu.org/licenses/>.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Workflow
/// </summary>
public class Workflow
{
    public int WfID { get; set; }
    public string WfName { get; set; }
    public List<int> Methods { get; set; }
    public List<string> MethodName { get; set; }
    public string WrkflwMethods { get; set; }
    public string[] Inputs { get; set; }
    public List<string> OutputParam { get; set; }
    public List<string> InputParam { get; set; }
    public string WrkflwInputs { get; set; }
	public Workflow()
	{
        MethodName = new List<string>();
        Methods = new List<int>();
        InputParam = new List<string>();
        OutputParam = new List<string>(); 
	}
}