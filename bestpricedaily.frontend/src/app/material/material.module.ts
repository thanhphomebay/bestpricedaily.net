import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

const COMPs = [
  // MatButtonModule, MatCardModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatToolbarModule, MatListModule
  MatButtonModule, MatCardModule, MatFormFieldModule, MatIconModule, MatMenuModule, MatProgressSpinnerModule, MatToolbarModule, MatTooltipModule, MatListModule
]
@NgModule({
  imports: COMPs,
  exports: COMPs
})
export class MaterialModule { }
