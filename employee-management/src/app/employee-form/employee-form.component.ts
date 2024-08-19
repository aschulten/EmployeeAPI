import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Employee } from '../models/employee.model';

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form.component.html',
  styleUrls: ['./employee-form.component.css']
})
export class EmployeeFormComponent {
  @Input() employee: Employee | null = null;
  @Output() employeeSubmit = new EventEmitter<Employee>();

  employeeForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.employeeForm = this.fb.group({
      // Form controls for employee properties
    });
  }

  ngOnInit() {
    if (this.employee) {
      this.employeeForm.patchValue(this.employee);
    }
  }

  onSubmit() {
    if (this.employeeForm.valid) {
      this.employeeSubmit.emit(this.employeeForm.value);
    }
  }
}