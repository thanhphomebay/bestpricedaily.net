import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AppState, getIsLoading } from './app.store';
import { tap } from 'rxjs/operators';
import { cartCaptureOrderStatusRequest } from './cart/cart.store';
import { CartService } from './cart/cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'cart';
  constructor(private store: Store<AppState>, private cartService : CartService) { }
  isLoading$ = this.store.pipe(select(getIsLoading));
  ngOnInit() {
    this.store.dispatch(cartCaptureOrderStatusRequest());
    // this.cartService.getOrderStatus('7GY').subscribe(
    // this.cartService.getOrderStatus('7GY45449R9899041T').subscribe(
      // x=> console.log(x)
    // )
  }
}
