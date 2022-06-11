import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { DepartmentComponent } from '../department/department.component';
@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  constructor( private http : HttpClient) { }

  departments : any=[];
  employees:any=[];
  modalTitle="";
  EmployeeId = 0;
  EmployeeName = "";
  Department="";
  DateOfJoining="";
  PhotoFileName="user.png";
  PhotoPath = environment.PHOTO_URL;

  ngOnInit(): void {
    this.refereshList();
  }


  refereshList()
  {
    this.http.get<any>(environment.API_URL+'employee')
    .subscribe(data=>{
      this.employees = data;
    });

    this.http.get<any>(environment.API_URL+'department')
    .subscribe(data=>{
      this.departments = data;
    });
  }

  addClick()
  {
    this.modalTitle="Added Employee Successfully";
    this.EmployeeId=0;
    this.EmployeeName = "";
    this.Department="";
    this.DateOfJoining="";
    this.PhotoFileName="user.png";
  }

  editClick(emp:any)
  {
    this.modalTitle="Edited Employee Successfully";
    this.EmployeeId=emp.EmployeeId;
    this.EmployeeName = emp.EmployeeName;
    this.Department=emp.Deaprtment;
    this.DateOfJoining=emp.DateOfJoining;
    this.PhotoFileName=emp.PhotoFileName;
  }

  createClick()
  {
  var val ={
    EmployeeName : this.EmployeeName,
    Department : this.Department,
    DateOfJoining : this.DateOfJoining,
    PhotoFileName : this.PhotoFileName

  };

  this.http.post(environment.API_URL+ 'employee', val)
  .subscribe(res=>{
    alert(res.toString());
    this.refereshList();
  });
  }

updateClick()
  {
    var val ={
      EmployeeId : this.EmployeeId,
      EmployeeName : this.EmployeeName,
    Department : this.Department,
    DateOfJoining : this.DateOfJoining,
    PhotoFileName : this.PhotoFileName
      
    };
  
    this.http.put(environment.API_URL+ 'employee', val)
    .subscribe(res=>{
      alert(res.toString());
      this.refereshList();
    });
  }
  deleteClick(id:any)
  {
    if(confirm("Are you Sure?"))
    {
    this.http.delete(environment.API_URL+ 'employee/'+id)
    .subscribe(res=>{
      alert(res.toString());
      this.refereshList();
    });
  }
}
}
