﻿namespace CarDealerApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Models.BindingModels;
    using CarDealer.Models.ViewModels;
    using CarDealer.Services;
    using CarDealerApp.Security;

    [RoutePrefix("cars")]
    public class CarsController : Controller
    {
        private CarsService service;

        public CarsController()
        {
            this.service = new CarsService();
        }

        [HttpGet]
        [Route("details/{id}")]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "OutOfRangeError")]
        public ActionResult Details(int id)
        {   
            var context = new CarDealerContext();
            var car = context.Cars.Find(id);
            if (car == null)
            {
                throw new ArgumentOutOfRangeException(nameof(car), id, "There is no such car.");
            }
            else if (car.TravelledDistance > 1000000)
            {
                throw new InvalidOperationException("The car is too old to be driven!");
            }

            return this.View();
        }


        [HttpGet]
        [Route("{make?}")]
        public ActionResult All(string make)
        {
            IEnumerable<CarVm> modelCarVms = this.service.GetCarsFromGivenMakeInOrder(make);
            return this.View(modelCarVms);
        }

        [HttpGet]
        [Route("{id:int}/parts")]
        public ActionResult About(int id)
        {
            AboutCarVm vm = this.service.GetCarWithParts(id);

            return this.View(vm);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            return this.View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance, Parts")] AddCarBm bind)
        {
            var httpCookie = this.Request.Cookies.Get("sessionId");
            if (httpCookie == null || !AuthenticationManager.IsAuthenticated(httpCookie.Value))
            {
                return this.RedirectToAction("Login", "Users");
            }

            if (this.ModelState.IsValid)
            {
                this.service.AddCar(bind);

                return this.RedirectToAction("All");
            }

            return this.View();
        }
    }
}