import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReceiptComponent } from './receipt.component';
import { Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: ReceiptComponent }
]

@NgModule({
  declarations: [ReceiptComponent],
  imports: [
    CommonModule
  ]
})
export class ReceiptModule { }
