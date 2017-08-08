﻿using System.ComponentModel.DataAnnotations;

namespace TMS.TodoApi
{
    public enum TaskStatus : byte
    {
        [Display(Name = "Вільна")]
        None,

        [Display(Name = "В процесі")]
        InProgress,

        [Display(Name = "Перероблюється")]
        Recycle,

        [Display(Name = "Виконана")]
        Completed
    }
}