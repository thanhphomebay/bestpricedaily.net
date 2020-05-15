import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-not-found',
  template: `
    <mat-card>
      <mat-card-title>404 NOT FOUND </mat-card-title>
      <mat-card-content> <button mat-button routerLink='/home'>Home</button> </mat-card-content>
    </mat-card>
  `,
  styles: [`
  :host {
    text-align: center;
  }
  `]
})
export class NotFoundComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
