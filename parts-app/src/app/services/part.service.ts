import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PartService {

  private api = 'http://localhost:8080/api/parts';

  constructor(private http: HttpClient) {}

  getParts() {
    return this.http.get<any[]>(this.api);
  }

  createPart(part: any) {
    return this.http.post(this.api, part);
  }

  deletePart(id: string) {
    return this.http.delete(`${this.api}/${id}`);
  }
}