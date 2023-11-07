using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApp.Pattern.Models
{
    //Setting MyTask Properties
    [AddINotifyPropertyChangedInterface]
    public class TaskItem
    {
        public string Task_Name { get; set; }
        public bool CompletedTasks { get; set; }
        public int Cat_Id { get; set; }
        public string Task_Color { get; set; }
        public DateTime Date { get; set; }
        public bool remindertime { get; set; }
    }
}
