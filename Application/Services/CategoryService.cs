using AutoMapper;
using CleanProject.Application.Interfaces;
using CleanProject.Application.ViewModels.Category;
using CleanProject.Domain.Interfaces;
using CleanProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanProject.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }


        public bool DeleteAllCategory()
        {
            return categoryRepository.DeleteAll();
        }

        public void DeleteCategory(int Id)
        {
            categoryRepository.Delete(Id);
        }
        public IEnumerable<Category> GetAllCategory()
        {
            var result = categoryRepository.GetAll();
            return result;
        }

        public Category GetCategory(int Id)
        {
            var result = categoryRepository.GetById(Id);
            return result;
        }

        public CategoryViewModel GetCategoryById(int Id)
        {
            var result = categoryRepository.GetById(Id);
            var result2 = mapper.Map<CategoryViewModel>(result);
            return result2;
        }

        public void InsertCategory(Category category)
        {
            categoryRepository.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            categoryRepository.Update(category);
        }
    }
}
