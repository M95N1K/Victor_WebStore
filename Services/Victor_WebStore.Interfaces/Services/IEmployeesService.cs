﻿using System.Collections.Generic;
using Victor_WebStore.Domain.ViewModels;

namespace Victor_WebStore.Interfaces.Services
{
    public interface IEmployeesService
    {
        /// <summary>
        /// Получение списка всех сотрудников
        /// </summary>
        /// <returns></returns>
        IEnumerable<EmployeeViewModel> GetAll();

        /// <summary>
        /// Данные одного рабочего
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EmployeeViewModel GetById(int id);

        /// <summary>
        /// Запись данных в БД
        /// </summary>
        void Commit();

        /// <summary>
        /// Редактирование работника
        /// </summary>
        /// <param name="persona"></param>
        void Edit(EmployeeViewModel persona);

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="persona"></param>
        void AddNew(EmployeeViewModel persona);

        /// <summary>
        /// Удаление пользователя по id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
