﻿using System.Collections.Generic;
using AspNetCoreSpa.Domain.Entities;
using AspNetCoreSpa.Infrastructure;
using AspNetCoreSpa.Infrastructure.Services.Uow;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductViewModel = Web.ViewModels.ProductViewModel;

namespace Web.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IUnitOfWork _uow;

        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        // GET: api/Product
        [HttpGet]
        public IActionResult Get()
        {
            var allProduct = _uow.Products.GetAll();
            return Ok(_mapper.Map<IEnumerable<ProductViewModel>>(allProduct));
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _uow.Products.Get(id);
            return Ok(_mapper.Map<ProductViewModel>(product));
        }

        // POST: api/Product
        [HttpPost]
        public void Post([FromBody] ProductViewModel product)
        {
            _uow.Products.Add(_mapper.Map<Product>(product));
            _uow.SaveChanges();
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] ProductViewModel product)
        {
            var p = _uow.Products.Get(id);
            p.Name = product.Name;
            p.Description = product.Description;
            p.Icon = product.Icon;
            p.BuyingPrice = product.BuyingPrice;
            p.SellingPrice = product.SellingPrice;
            p.UnitsInStock = product.UnitsInStock;
            p.IsActive = product.IsActive;
            p.IsDiscontinued = product.IsDiscontinued;
            _uow.Products.Update(p);
            var result = _uow.SaveChanges();
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _uow.Products.Remove(_uow.Products.Get(id));
            _uow.SaveChanges();
        }
    }
}
