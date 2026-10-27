using depiSession.Data;
using depiSession.Models;
using Microsoft.AspNetCore.Mvc;

namespace depiSession.Controllers;

public class StudentController (AppDBContext context): Controller {
    // GET
    public IActionResult Index() {
        // context.Students.ToList();
        return View( context.Students.ToList());
    }
    
    [HttpGet]
    public IActionResult Add() {
        return View();
    }
    
    [HttpPost]

    public IActionResult Add(Student s) {
        
        if (!ModelState.IsValid){
        return View(s);
            
        }

        context.Students.Add(s);
        context.SaveChanges();
     return   RedirectToAction("Index");
    }
    
    
    //
  
    
    public IActionResult Edit(Student s)
    {   
        if(!ModelState.IsValid){
            return View(s);
        }
        var student = context.Students.Find(s.Id);
        if(student == null) return NotFound();
        student.Name = s.Name;
        student.Age = s.Age;
        student.Email = s.Email;
        student.Department = s.Department;
      
        context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        var student = context.Students.Find(id);

        if (student == null) 
        {
            return NotFound();
        }

        context.Students.Remove(student);
        context.SaveChanges();
        return RedirectToAction("Index");
    }
}