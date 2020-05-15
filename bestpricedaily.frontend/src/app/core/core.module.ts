import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import { NotFoundComponent } from './not-found.component';
import { MaterialModule } from '../material/material.module';
import { ToolbarComponent } from './toolbar.component';
import { RouterModule } from '@angular/router';
import { SpinnerComponent } from './spinner.component';
import { FlexLayoutModule } from '@angular/flex-layout';



@NgModule({
  declarations: [HomeComponent, NotFoundComponent, ToolbarComponent, SpinnerComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    FlexLayoutModule
  ],
  exports: [ToolbarComponent, SpinnerComponent]
})
export class CoreModule { }
