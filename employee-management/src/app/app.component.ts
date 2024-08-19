import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { EmployeeService } from './services/employee.service';
import { Employee } from './models/employee.model';
import { EmployeeListComponent } from './employee-list/employee-list.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [CommonModule, EmployeeListComponent]
})
export class AppComponent implements OnInit {
  employees: Employee[] = [];
  pageNumber: number = 1;
  pageSize: number = 30;

 
  constructor(private http: HttpClient, private employeeService: EmployeeService) {}

  ngOnInit() {
    this.getEmployees(this.pageNumber, this.pageSize);
  }

  getEmployees(pageNumber: number,pageSize: number) {
    this.employeeService.getEmployees(pageNumber,pageSize)
      .subscribe(
        data => {this.employees = data.data},
        error => console.error('Error fetching employees', error)
      );
  }
}

