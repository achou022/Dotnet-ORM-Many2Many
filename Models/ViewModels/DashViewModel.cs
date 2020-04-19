using System;
using System.Collections.Generic;
using WeddingPlanner.Models;

namespace WeddingPlanner.Models.ViewModels
{
    public class DashVM
    {
        public int loggedUser {get; set;}
        public List<Wedding> AllWeddings {get; set;}
    }
}