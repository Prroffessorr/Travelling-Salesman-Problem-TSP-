//////////////////////////////////////////////////////////////////////////////////////////////////
// File Name: Cities.cs
//      Date: 06/01/2006
// Copyright (c) 2006 Michael LaLena. All rights reserved.  (www.lalena.com)
// Permission to use, copy, modify, and distribute this Program and its documentation,
//  if any, for any purpose and without fee is hereby granted, provided that:
//   (i) you not charge any fee for the Program, and the Program not be incorporated
//       by you in any software or code for which compensation is expected or received;
//   (ii) the copyright notice listed above appears in all copies;
//   (iii) both the copyright notice and this Agreement appear in all supporting documentation; and
//   (iv) the name of Michael LaLena or lalena.com not be used in advertising or publicity
//          pertaining to distribution of the Program without specific, written prior permission. 
///////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
namespace Tsp
{
    /// <summary>
    /// This class contains the list of cities for this test.
    /// Each city has a location and the distance information to every other city.
    /// </summary>
    public class Cities : List<City>
    {
        /// <summary>
        /// Finding latitude and longitude to work with kml files
        /// </summary>
        static string[] split_cordinates_longitude = null;
        static string[] split_cordinates_latitude = null;
        /// <summary>
        /// Determine the required format
        /// </summary>
        TspForm tspform = new TspForm();

        /// <summary>
        /// Determine the distances between each city.
        /// </summary>
        /// <param name="numberOfCloseCities">When creating the initial population of tours, this is a greater chance
        /// that a nearby city will be chosen for a link. This is the number of nearby cities that will be considered close.</param>
        public void CalculateCityDistances( int numberOfCloseCities )
        {
            foreach (City city in this)
            {
                city.Distances.Clear();

                for (int i = 0; i < Count; i++)
                {
                    city.Distances.Add(Math.Sqrt(Math.Pow((double)(city.Location.X - this[i].Location.X), 2D) +
                                       Math.Pow((double)(city.Location.Y - this[i].Location.Y), 2D)));
                }
            }

            foreach (City city in this)
            {
                city.FindClosestCities(numberOfCloseCities);
            }
        }

        /// <summary>
        /// Open the XML file that contains the list of cities.
        /// </summary>
        /// <param name="fileName">Name of the XML file.</param>
        /// <returns>The city list.</returns>
        /// <exception cref="FileNotFoundException">fileName parameter is invalid.</exception>
        /// <exception cref="InvalidCastException">XML File is not properly formatted.</exception>
        public void OpenCityList(string fileName)
        {
            DataSet cityDS = new DataSet();

            try
            {
                if (tspform.use_xml_or_kml == true)
                {
                    this.Clear();

                    cityDS.ReadXml(fileName);

                    DataRowCollection cities = cityDS.Tables[0].Rows;

                    foreach (DataRow city in cities)
                    {
                        this.Add(new City(Convert.ToInt32(city["X"], CultureInfo.CurrentCulture), Convert.ToInt32(city["Y"], CultureInfo.CurrentCulture)));
                    }
                }

                else
                {
                    this.Clear();

                    XmlDocument xml_doc = new XmlDocument();

                    xml_doc.Load(fileName);

                    XmlNodeList Main_nodes = xml_doc.GetElementsByTagName("Document");

                    foreach (XmlNode First_child in Main_nodes)
                    {
                        foreach (XmlNode Second_child in First_child.ChildNodes)
                        {
                            if (Second_child.Name == "Placemark")
                            {
                                foreach (XmlNode Thrid_child in Second_child.ChildNodes)
                                {
                                    if (Thrid_child.Name == "LookAt")
                                    {
                                        foreach (XmlNode Fourth_child in Thrid_child.ChildNodes)
                                        {

                                            if (Fourth_child.Name == "longitude")//X
                                            {
                                                split_cordinates_longitude = Fourth_child.InnerText.Split('.');
                                                Console.WriteLine(split_cordinates_longitude[1]);
                                            }
                                            else if (Fourth_child.Name == "latitude")//Y
                                            {
                                                split_cordinates_latitude = Fourth_child.InnerText.Split('.');
                                                Console.WriteLine(split_cordinates_latitude[1]);
                                            }

                                        }

                                        this.Add(new City(Convert.ToInt32(split_cordinates_longitude[0], CultureInfo.CurrentCulture), Convert.ToInt32(split_cordinates_latitude[0], CultureInfo.CurrentCulture)));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            finally
            {
                cityDS.Dispose();
            }
        }
    }
}
