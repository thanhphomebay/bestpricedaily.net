import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-spinner',
  template: `
  <ng-container *ngIf="overlay;else progressSpinner">
    <div class="overlay">
        <div class="center">
            <ng-template [ngTemplateOutlet]="progressSpinner"></ng-template>
        </div>
    </div>
  </ng-container>
  <ng-template #progressSpinner>
      <mat-progress-spinner 
      [diameter]="diameter"
      [mode]="mode" 
      [color]="color"
      [strokeWidth]="strokeWidth"
      [value]="value">
      </mat-progress-spinner>
  </ng-template>
  `,
  styles: [`
  .center {
    position: absolute;
    top: 50%;
    left: 50%;
    -moz-transform: translateX(-50%) translateY(-50%);
    -webkit-transform: translateX(-50%) translateY(-50%);
    transform: translateX(-50%) translateY(-50%);
  }
  .overlay{
    height:100vh;
    width:100%;
    background-color:rgba(0, 0, 0, 0.286);
    z-index:    10;
    top:        0; 
    left:       0; 
    position:   fixed;
  }
  `] 
})
export class SpinnerComponent implements OnInit {
  @Input() value : number = 100;
  @Input() diameter: number = 100;
  @Input() mode : string ="indeterminate";
  @Input() strokeWidth : number = 10;
  @Input() overlay: boolean = false;
  @Input() color: string = "primary";

  constructor() { }

  ngOnInit(): void {
  }

}
