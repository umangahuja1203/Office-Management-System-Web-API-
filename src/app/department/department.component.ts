import { Component, OnInit } from '@angular/core';

import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  constructor( private http : HttpClient) { }

  departments : any=[];
  modalTitle="";
  DepartmentID = 0;
  DepartmentName = "";
  DepartmentIdFilter="";
  DepartmentNameFilter="";
  DepartmentsWithoutFilter:any=[];

  ngOnInit(): void {
    this.refereshList();
  }


  refereshList()
  {
    this.http.get<any>(environment.API_URL+'department')
    .subscribe(data=>{
      this.departments = data;
      this.DepartmentsWithoutFilter = data;
    });
  }

  addClick()
  {
    this.modalTitle="Added Deaprtment Successfully";
    this.DepartmentID=0;
    this.DepartmentName="";
  }

  editClick(dept:any)
  {
    this.modalTitle="Edited Deaprtment Successfully";
    this.DepartmentID=dept.DepartmentID;
    this.DepartmentName=dept.DepartmentName;
  }

  createClick()
  {
  var val ={
    DepartmentName :  this.DepartmentName
  };

  this.http.post(environment.API_URL+ 'department', val)
  .subscribe(res=>{
    alert(res.toString());
    this.refereshList();
  });
  }

updateClick()
  {
    var val ={
      DepartmentID : this.DepartmentID,
      DepartmentName :  this.DepartmentName
      
    };
  
    this.http.put(environment.API_URL+ 'department', val)
    .subscribe(res=>{
      alert(res.toString());
      this.refereshList();
    });
  }
  deleteClick(id:any)
  {
    if(confirm("Are you Sure?"))
    {
    this.http.delete(environment.API_URL+ 'department/'+id)
    .subscribe(res=>{
      alert(res.toString());
      this.refereshList();
    });
  }
}

FilterFn()
{
var DepartmentIdFilter = this.DepartmentIdFilter;
var DepartmentNameFilter = this.DepartmentNameFilter;

this.departments = this.DepartmentsWithoutFilter.filter(
  function(el:any)
  {
    return el.DepartmentID.toString().toLowerCase().includes(
      DepartmentIdFilter.toString().trim().toLowerCase()
    ) && 
    el.DepartmentName.toString().toLowerCase().includes(
      DepartmentNameFilter.toString().trim().toLowerCase()
    )
  }
)
}

sortResult(prop:any ,asc :any)
{
   this.departments = this.DepartmentsWithoutFilter.sort(
     function(a:any , b:any)
     {
          if(asc)
          {
            return (a[prop]>b[prop])?1:((a[prop]<b[prop])?-1:0);
          }
          else{
            return (b[prop]>a[prop])?1:((b[prop]<a[prop])?-1:0);
          }
     }
   )
}
}
