//////////////////////////////////////////////////////////////////////////////////////////////////
// File Name: TspEventArgs.cs
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
using System.Windows.Forms;
using System.Drawing;

namespace Tsp
{
    /// <summary>
    /// Event arguments when the TSP class wants the GUI to draw a tour.
    /// </summary>
    public class TspEventArgs : EventArgs
    {
        /// <summary>
        /// Default Constructor.
        /// </summary>
        public TspEventArgs()
        {
        }

        /// <summary>
        /// Constructor that sets all the properties.
        /// </summary>
        /// <param name="cityList">The list of cities to draw.</param>
        /// <param name="bestTour">The tour that connects all the cities.</param>
        /// <param name="generation">Which generation is this.</param>
        /// <param name="complete">Is this the last update before we are done.</param>
        public TspEventArgs(Cities cityList, Tour bestTour, int generation, bool complete)
        {
            this.cityList = cityList;
            this.bestTour = bestTour;
            this.generation = generation;
            this.complete = complete;
        }

        /// <summary>Private copy of the list of cities.</summary>
        private Cities cityList;
        /// <summary>Public property for list of cities.</summary>
        public Cities CityList
        {
            get
            {
                return cityList;
            }
        }

        /// <summary>Private copy of the tour of the cities.</summary>
        private Tour bestTour;
        /// <summary>Public property for the tour of the cities.</summary>
        public Tour BestTour
        {
            get
            {
                return bestTour;
            }
        }

        /// <summary>Private copy for which generation this tour came from.</summary>
        private int generation;
        /// <summary>Public property for which generation this tour came from.</summary>
        public int Generation
        {
            get
            {
                return generation;
            }
            set
            {
                generation = value;
            }
        }

        /// <summary>Private copy indicating if the TSP algorithm is complete.</summary>
        private bool complete = false;
        /// <summary>Public property indicating if the TSP algorithm is complete.</summary>
        public bool Complete
        {
            get
            {
                return complete;
            }
            set
            {
                complete = value;
            }
        }
    }
}