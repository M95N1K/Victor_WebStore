using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Victor_WebStore.ViewModels;

namespace Victor_WebStore.Infrastructure.Interfaces
{
    public interface IProductsService
    {
        /// <summary>
        /// Получение списка всех товаров
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductsViewModel> GetAll();

        /// <summary>
        /// Данные одного товара
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ProductsViewModel GetById(int id);

        /// <summary>
        /// Запись данных в БД
        /// </summary>
        void Commit();

        /// <summary>
        /// Добавление товара
        /// </summary>
        /// <param name="persona"></param>
        void AddNew(ProductsViewModel persona);

        /// <summary>
        /// Удаление товара по id
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
    }
}
