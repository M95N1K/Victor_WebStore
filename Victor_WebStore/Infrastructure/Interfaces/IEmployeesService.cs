using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Infrastructure.Interfaces
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
