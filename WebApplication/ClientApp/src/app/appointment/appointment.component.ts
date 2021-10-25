import { Component, OnInit } from '@angular/core';
import { ApiService } from './api.service';
import { AppointmentService } from './appointment.service';

@Component({
  selector: 'appointment',
  templateUrl: './appointment.component.html',
  styleUrls: ['./appointment.component.css']
})
export class AppointmentComponent implements OnInit {
  pagination: Pagaination;
  appointments = [];
  pageNo: any = 1;
  pageNumber: boolean[] = [];
  currentPage: any = 0;
  sortOrder: any = 'CompanyName';

  //Appointment Variables
  pageField = [];
  exactPageList: any;
  appointmentData: number;
  appointmentsPerPage: any = 5;
  totalAppointments: any;
  totalAppointmentsCount: any;

  constructor(public service: ApiService, public appointmentService: AppointmentService) { }

  ngOnInit() {
    this.pageNumber[0] = true;
    this.appointmentService.temppage = 0;
    this.getAllAppointments();
  }

  getAllAppointments() {
    this.service.getAllAppointments(this.pageNo, this.appointmentsPerPage, this.sortOrder).subscribe(data => {
      console.log(data.body);
      console.log(data.headers.get('X-Pagination'));
      this.appointments = data.body;
      this.getAllAppointmentsCount(data.headers);
    })
  }

  //Method For Appointment
  totalNoOfPages() {
    this.appointmentData = Number(this.totalAppointmentsCount / this.appointmentsPerPage);
    let tempPageData = this.appointmentData.toFixed();
    console.log(tempPageData);
    if (Number(tempPageData) < this.appointmentData) {
      this.exactPageList = Number(tempPageData) + 1;
      this.appointmentService.exactPageList = this.exactPageList;
    } else {
      this.exactPageList = Number(tempPageData);
      this.appointmentService.exactPageList = this.exactPageList
    }
    this.appointmentService.pageOnLoad();
    this.pageField = this.appointmentService.pageField;
  }

  showAppointmentsByPageNumber(page, i) {
    this.appointments = [];
    this.pageNumber = [];
    this.pageNumber[i] = true;
    this.pageNo = page;
    this.getAllAppointments();
  }

  getAllAppointmentsCount(headers: any) {
    this.pagination = JSON.parse(headers.get('X-Pagination'));
    this.currentPage = this.pagination.CurrentPage;
    this.totalAppointmentsCount = this.pagination.TotalCount;
    this.totalNoOfPages();
    // this.service.getAllAppointmentsCount().subscribe((res: any) => {
    //   this.totalAppointmentsCount = res;
    //   this.totalNoOfPages();
    // })
  }
}

interface Pagaination {
  TotalCount: number;
  PageSize: number;
  CurrentPage: number;
  HasNext: boolean;
  HasPrevious: boolean;
}
