import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private url = "";

  constructor(public http: HttpClient, @Inject('BASE_URL') public baseUrl: string) {
  }

  getAllAppointments(pageNo, pageSize, sortOrder): Observable<any> {
    this.url = this.baseUrl + 'appointment?pageNumber=' + pageNo + '&pageSize=' + pageSize + '&sortOrder=' + sortOrder;
    return this.http.get(this.url, {observe: 'response'});
  }

  // getAllAppointmentsCount(): Observable<any> {
  //   this.url = 'http://localhost:59390/api/Pagination/getAllAppointmentsCount';
  //   return this.http.get(this.url);
  // }
}
