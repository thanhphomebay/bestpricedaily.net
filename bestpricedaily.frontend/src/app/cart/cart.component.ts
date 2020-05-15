import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { getCartItems, getCartCount, AddItemToCart, RemoveItemFromCart, cartCreateOrderRequest, cartCaptureOrderStatusRequest } from './cart.store';
import { Item } from '../items/item.model';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import * as signalR from '@microsoft/signalr';
import { CartItem } from './cart-item.model';
import { AppState } from '../app.store';
import { CartService } from './cart.service';
// declare var paypal;

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  texasSaleTax: number = 7.25;
  shippingCost: number = 5.00;
  cartItems$ = this.store.pipe(select(getCartItems));
  getCartCount$ = this.store.pipe(select(getCartCount));
  paidFor: boolean = false;
  private baseUrl: string = undefined;
  @ViewChild('paypalcheckout', { static: false }) paypalElementRef: ElementRef;
  greeting: string;
  // dataChannel = new signalR.HubConnectionBuilder().withUrl("api/datachannel").build();


  constructor(@Inject('BASE_URL') baseUrl: string, private store: Store<AppState>, private router: Router, private http: HttpClient, private cartService: CartService) {
    this.baseUrl = baseUrl;
  }
  // async ngOnInit() {
  //   this.dataChannel.on("cartcomponent", data => { this.greeting = data; });

  //   await this.dataChannel.start();
  //   this.dataChannel.invoke("SendMessage", "Joe");
  // }
  async ngOnInit() {
    this.cartService.getAll().subscribe(
      res => { this.texasSaleTax = res.tax_rate; this.shippingCost = res.shipping_base_rate }
    )

    // let connection = new signalR.HubConnectionBuilder()
    //   .withUrl("/datachannel")
    //   .build();

    // connection.on("cartcomponent", data => {
    //   this.greeting = data;
    // });

    // await connection.start()
    // connection.invoke("SendMessage", "Joe");
  }
  ngAfterViewInit() {
  }
  removeItem(item: CartItem) {
    this.store.dispatch(RemoveItemFromCart({ item }));
  }
  addItem(item: Item) {
    this.store.dispatch(AddItemToCart({ item }));
  }
  checkoutWithPaypal() {    
    this.store.dispatch(cartCreateOrderRequest());
  }

  // checkoutWithPaypal(items: CartItem[]) {
  //   console.log(this.baseUrl);

    // this.http.post(this.baseUrl + 'api/createorder', {
    //    items: [{
    //     quantity: '1',
    //     sku: 'haf001'
    //   }, {
    //     name: 'tranchau',
    //     quantity: '1',
    //     sku: 'dsc002'
    //   }]
    //   items
    // }).subscribe(
    //   (res: any) => {
    //     // this.router.navigate(['/paypalconfirm']);
    //     // var popup = window.open(window.location.href = res.links[1].href, '_blank');//, 'location=yes,width=800,height=600');
    //     // window.location.href = res.links[1].href;
    //     // debugger;
    //     console.log('sucess senidng create payment' + console.log(JSON.stringify(res)))
    //     const [approvalUrl,] = res.links.filter((l: any) => l.rel == "approve");
    //     // console.log(approvalUrl);
    //     window.location.href = approvalUrl.href;
    //     // window.open(res.links[1].href, '_blank');
    //   },
    //   (err) => console.log('failure to create payment')
    // )
  // }
  calSubtotal(items: CartItem[]) {
    let subTotal = 0;
    for (let i = 0; i < items.length; i++) {
      subTotal = subTotal + items[i].price * items[i].quantity;
    }
    return subTotal;
  }
  calTax(items: CartItem[]) {
    return (this.texasSaleTax * this.calSubtotal(items)) / 100;
  }
  calTotal(items: CartItem[]) {
    const subtotal: number = this.calSubtotal(items);
    const tax: number = this.calTax(items);
    // return items.reduce((a, b) => a + (b.price * b.quantity), 0);
    return subtotal + tax + this.shippingCost;
  }
}
