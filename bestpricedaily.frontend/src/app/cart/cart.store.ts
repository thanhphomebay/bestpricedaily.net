

import { createAction, props, createReducer, createFeatureSelector, createSelector } from "@ngrx/store";
import { on } from '@ngrx/store';
import { Item } from "../items/item.model";
import { CartItem } from "./cart-item.model";

//Action
export const AddItemToCart = createAction('[cart] add item', props<{ item: Item }>());
export const RemoveItemFromCart = createAction('[cart] remove item', props<{ item: CartItem }>());

// export const CartCreateOrderRequest = createAction('[cart] create order request', props<{cartItems : CartItem[]}>());
export const cartCreateOrderRequest = createAction('[cart] create order request');//, props<{ cartItems: CartItem[] }>());
export const cartCreateOrderRequestSuccess = createAction('[cart] create order request success', props<{ payload : any }>());
export const cartCreateOrderRequestFailure = createAction('[cart] create order request failure', props<{errMsg : any}>());

export const cartCaptureOrderStatusRequest = createAction('[cart] capture order request');//, props<{ cartItems: CartItem[] }>());
export const cartCaptureOrderStatusRequestSuccess = createAction('[cart] capture order request success', props<{ orderid: string }>());
export const cartCaptureOrderStatusRequestFailure = createAction('[cart] capture order request failure', props<{ errMsg: any }>());

//Reducer
export interface CartState {
  orderId: string,
  cart: CartItem[];
}
const initState: CartState = {
  orderId: "",
  cart: [],
}

export const cartReducer = createReducer(
  initState,
  on(cartCaptureOrderStatusRequestSuccess, (s, a) => { debugger; if (s.orderId === a.orderid) return initState;  else return s }),
  on(cartCreateOrderRequestSuccess, (s, a) => ({...s, orderId : a.payload.id})),
  on(AddItemToCart, (s, a) => {
    const cartItem = Object.assign({ quantity: 1 }, { id: a.item.id, price: a.item.price, name: a.item.name, pix: a.item.pix }) as CartItem;
    const found = s.cart.find(k => k.id == cartItem.id) as CartItem;
    const items = s.cart.filter(x => x.id != cartItem.id);
    if (found)
      cartItem.quantity = found.quantity + 1;
    items.push(cartItem);
    items.sort((a, b) => a.id > b.id ? 1 : -1);
    return { ...s, cart: items }
  }),
  on(RemoveItemFromCart, (s, a) => {
    const item = Object.assign({}, a.item) as CartItem;
    item.quantity--;
    const items = s.cart.filter(k => k.id != item.id);
    if (item.quantity > 0) {
      items.push(item);
    }
    items.sort((a, b) => a.id > b.id ? 1 : -1);
    return { ...s, cart: items };
  }),
)

//selector

const getCartState = createFeatureSelector<CartState>('cart');
export const getCartCount = createSelector(getCartState, s => s && s.cart && s.cart.reduce((sum, x) => sum + x.quantity, 0));
export const getCartItems = createSelector(getCartState, s => s && s.cart);
export const getCartOrderId = createSelector(getCartState, s => s && s.orderId);

