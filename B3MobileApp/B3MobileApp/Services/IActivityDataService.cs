﻿using System.Threading.Tasks;
using B3MobileApp.Model;

namespace B3MobileApp.Services
{
    public interface IActivityDataService
    {
        Task<string> SaveActivity(Activity activity);
    }
}