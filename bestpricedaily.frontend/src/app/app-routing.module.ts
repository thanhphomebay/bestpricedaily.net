import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './core/home.component';
import { NotFoundComponent } from './core/not-found.component';


const routes: Routes = [
  { path: '', redirectTo: 'items', pathMatch: 'full' },
  { path: 'home', component: HomeComponent },
  { path: 'items', loadChildren: () => import('./items/items.module').then(m => m.ItemsModule) },
  { path: 'cart', loadChildren: () => import('./cart/cart.module').then(m => m.CartModule) },
  // { path: 'paypalconfirm', loadChildren: () => import('./paypalconfirm/paypalconfirm.module').then(m => m.PaypalconfirmModule) },
  { path: 'receipt', loadChildren: () => import('./receipt/receipt.module').then(m => m.ReceiptModule) },
  { path: 'contact', loadChildren: () => import('./contact/contact.module').then(m => m.ContactModule) },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
