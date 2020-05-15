import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MyStoreSetting } from './mystoresetting.model';
import { Item } from '../items/item.model';
import { CartItem } from './cart-item.model';
@Injectable({
  providedIn: 'root'
})
export class CartService {

  baseUrl: string;
  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient) { this.baseUrl = baseUrl; }
  getAll(): Observable<MyStoreSetting> {
    // return ITEMS_MOCK;
    return this.http.get<MyStoreSetting>(this.baseUrl + 'api/mystoresetting');
  }
  getCommitted() {
    //return this.http.get<
  }
  createOrder(items: CartItem[]) {
    return this.http.post(this.baseUrl + 'api/createorder', { items }); /*.subscribe(
      (res: any) => {
        // this.router.navigate(['/paypalconfirm']);
        // var popup = window.open(window.location.href = res.links[1].href, '_blank');//, 'location=yes,width=800,height=600');
        // window.location.href = res.links[1].href;
        // debugger;
        console.log('sucess senidng create payment' + console.log(JSON.stringify(res)))
        const [approvalUrl,] = res.links.filter((l: any) => l.rel == "approve");
        // console.log(approvalUrl);
        window.location.href = approvalUrl.href;
        // window.open(res.links[1].href, '_blank');
      },
      (err) => console.log('failure to create payment')
    )*/
  }
  getOrderStatus(orderid: string) {
    debugger;
    return this.http.get<string>(this.baseUrl + `api/captureorderstatus/${orderid}`);
  }
}
