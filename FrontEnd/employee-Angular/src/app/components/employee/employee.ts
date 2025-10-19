import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; // ✅ Required for *ngFor, *ngIf
import { FormsModule } from '@angular/forms';
import { Employee } from '../../models/employee';
import { Employeeservice } from '../../services/employee';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, FormsModule], // ✅ must include CommonModule
  templateUrl: './employee.html',
  styleUrls: ['./employee.css']
})
export class EmployeeComponent {
  employees: Employee[] = [];
  selectedEmployee: Employee = { id:0, name:'', position:'', department:'', salary:0, email:'' };
  isModalOpen = false;
  isEditMode = false;

  constructor(private employeeService: Employeeservice) {
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getEmployees().subscribe(data => this.employees = data);
  }

  openModal(employee?: Employee) {
    this.isEditMode = !!employee;
    this.selectedEmployee = employee ? { ...employee } : { id:0, name:'', position:'', department:'', salary:0, email:'' };
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
  }

  saveEmployee() {
    if (this.isEditMode) {
      this.employeeService.updateEmployee(this.selectedEmployee).subscribe(() => {
        this.loadEmployees();
        this.closeModal();
      });
    } else {
      this.employeeService.createEmployee(this.selectedEmployee).subscribe(() => {
        this.loadEmployees();
        this.closeModal();
      });
    }
  }

  deleteEmployee(id: number) {
    if (confirm('Are you sure want to delete this employee?')) {
      this.employeeService.deleteEmployee(id).subscribe(() => this.loadEmployees());
    }
  }
}
