﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.Collections.Generic;

namespace CSWorkListService.Model
{
    public interface IWorklistItemsSource
    {

        /// <summary>
        /// this method queries some source like database or webservice to get a list of all scheduled worklist items.
        /// This method is called periodically.
        /// </summary>
        List<WorklistItem> GetAllCurrentWorklistItems();

    }
}
