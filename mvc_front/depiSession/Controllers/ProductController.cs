using depiSession.Models;
using Microsoft.AspNetCore.Mvc;

namespace depiSession.Controllers;

public class ProductController : Controller {
    [HttpGet]
    public IActionResult Add() {
        return View();
    }
    
    [HttpPost]
    public IActionResult Add(Product p) {
        if (!ModelState.IsValid){
            return View(p);
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Edit(int? id)
    {   
        return View(new Product());
    }

    [HttpPost]
    public IActionResult Edit(Product p)
    {   
        if(!ModelState.IsValid){
            return View(p);
        }

        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        return RedirectToAction("Index");
    }
}

