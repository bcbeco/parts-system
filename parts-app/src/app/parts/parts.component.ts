import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PartService } from '../services/part.service';

@Component({
  selector: 'app-parts',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './parts.component.html'
})
export class PartsComponent implements OnInit {

  parts: any[] = [];

  newPart = {
    name: '',
    serialNumber: '',
    price: 0,
    note: ''
  };

  constructor(private service: PartService) {}

  ngOnInit() {
    this.loadParts();
  }

  loadParts() {
    this.service.getParts().subscribe(data => {
      this.parts = data;
    });
  }

  addPart() {
    this.service.createPart(this.newPart).subscribe(() => {
      this.loadParts();
    });
  }

  deletePart(id: string) {
    this.service.deletePart(id).subscribe(() => {
      this.loadParts();
    });
  }
}