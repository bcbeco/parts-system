import { Component } from '@angular/core';
import { PartsComponent } from './parts/parts.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [PartsComponent],
  template: '<app-parts></app-parts>'
})
export class AppComponent {}