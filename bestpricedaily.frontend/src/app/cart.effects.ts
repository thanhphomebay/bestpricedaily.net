import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType, Effect } from '@ngrx/effects';
import { EMPTY, of } from 'rxjs';
import { map, exhaustMap, catchError, withLatestFrom, mergeMap, switchMap, tap } from 'rxjs/operators';
import { CartService } from './cart/cart.service';
import { cartCreateOrderRequest, cartCreateOrderRequestSuccess, getCartItems, cartCreateOrderRequestFailure, cartCaptureOrderStatusRequest, getCartOrderId, cartCaptureOrderStatusRequestSuccess, cartCaptureOrderStatusRequestFailure } from './cart/cart.store';
import { Store } from '@ngrx/store';
import { AppState } from './app.store';



@Injectable()
export class CartEffects {

  createOrderRequest$ = createEffect(() => {
    return this.actions$
      .pipe(
        ofType(cartCreateOrderRequest),
        withLatestFrom(this.store.select(getCartItems)),
        exhaustMap((boughtItem) => {
          //debugger;
          return this.cartService.createOrder(boughtItem[1])
            .pipe(
              map((resp: any) => cartCreateOrderRequestSuccess({ payload: resp })),
              catchError(error => of(cartCreateOrderRequestFailure({ errMsg: error })))
            )
        })
      )
  });
  @Effect({ dispatch: false })
  cartCreateOrderSuccess$ = this.actions$.pipe(
    ofType(cartCreateOrderRequestSuccess),
    tap((resp: any) => {
      //debugger;
      const [approvalUrl,] = resp.payload.links.filter((l: any) => l.rel == "approve");
      window.location.href = approvalUrl.href;
    })
  );

  captureOrderRequest$ = createEffect(() => {
    return this.actions$
      .pipe(
        ofType(cartCaptureOrderStatusRequest),
        withLatestFrom(this.store.select(getCartOrderId)),
        mergeMap((orderId) => {
          if(orderId[1]){
          return this.cartService.getOrderStatus(orderId[1])
            .pipe(
              map((resp: any) => { return cartCaptureOrderStatusRequestSuccess({ orderid: resp.order_id })}),
              catchError(error =>{ return of(cartCaptureOrderStatusRequestFailure({ errMsg: error }))})
            )
          }
          return EMPTY;//cart not yet checked out with paypal
        })
      )
  });
 

  constructor(
    private actions$: Actions,
    private cartService: CartService,
    private store: Store<AppState>,
  ) { }
}
