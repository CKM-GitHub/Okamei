﻿using System;
using System.Text;
using System.Web.Mvc;
using Models;
using OkameiProduction.BL;
using System.Data;

namespace OkameiProduction.Web.Controllers.HiuchiItiran
{
    public class HiuchiItiranController : BaseController
    {
        // GET: HiuchiItiran
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hiuchi()
        {
            var vm = new HiuchiItiranModel(); 
            ViewBag.ServerDate = DateTime.Now.ToString(DateTimeFormat.yyyyMMdd);
            SetDropDownListItems(vm); 
            return View(vm);
        } 
        private void SetDropDownListItems(HiuchiItiranModel vm)
        {
            CommonBL dl = new CommonBL();
            vm.TantouSitenSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouSiten);
            vm.TantouEigyouSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouEigyou); 
            vm.TantouCadSelectList = dl.GetMultiPorposeDDLItems(EMultiPorpose.TantouCad);  
        }
        public ActionResult DisplayResult()
        {
            var vm = GetFromQueryString<HiuchiItiranModel>();

            var bl = new HiuchiItiranBL();
            var dt = bl.GetDisplayResult(vm);

            ViewBag.Data = dt;

            return View(vm);
        }
    }
}