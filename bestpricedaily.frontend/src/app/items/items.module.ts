import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { MaterialModule } from '../material/material.module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { StoreModule } from '@ngrx/store';
import { ItemsListComponent as ItemListComponent } from './item-list/items-list.component';
import { ItemDetailComponent } from './item-detail/item-detail.component';
import { cartReducer } from '../cart/cart.store';

const routes: Routes = [
  {
    path: '', children: [
      { path: '', component: ItemListComponent },
      { path: 'detail/:id', component: ItemDetailComponent }
    ]
  }
]

@NgModule({
  declarations: [ItemListComponent, ItemDetailComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MaterialModule,
    FlexLayoutModule,
    StoreModule.forFeature('cart', cartReducer)
  ],
})
export class ItemsModule { }
