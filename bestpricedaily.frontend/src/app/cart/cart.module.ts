import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartComponent } from './cart.component';
import { MaterialModule } from '../material/material.module';
import { Routes, RouterModule } from '@angular/router';
import { FlexLayoutModule } from '@angular/flex-layout';
import { HttpClientModule } from '@angular/common/http';
import { EffectsModule } from '@ngrx/effects';
import { CartEffects } from '../cart.effects';

const routes: Routes = [
  { path: '', component: CartComponent }
]

@NgModule({
  declarations: [CartComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule.forChild(routes),
    FlexLayoutModule,
    HttpClientModule,
    // EffectsModule.forFeature([CartEffects])
  ],
})
export class CartModule { }
