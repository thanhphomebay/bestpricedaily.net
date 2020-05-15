import { Item } from "./item.model";
import { of, Observable } from "rxjs";
import { uuidv4 } from "./uuid";

/*export const ITEMS_MOCK: Observable<Item[]> = of(new Array(~~(Math.random() * 10)).fill({}).map((i: Item, x: number) => (
    {
        idx: x,
        uuid: uuidv4(),
        price: (Math.random() * 100 * x),
        name: `N95 NIOSH MASK WITH CE FDA`,
        desc: `This item costs ${x}.  It is now on sale for 10% off.  You should get it now`,
        // pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}`,
        pix: `https://picsum.photos/${~~(Math.random() * 1000)}/?random`,
    } as Item)
).reverse());
*/
const desc = `Tired of searching the best theme or template for your next Angular project? Yes, we know! There is a lot on the market but they not always fit your needs. Probably because of the design, the components or the code’s quality. Knowing this, we’ve gathered a list of 10+ cool Angular Material Themes which are using or are inspired by Google’s Material Design.`
export const ITEMS_MOCK: Observable<Item[]> = of([
    {
        id: uuidv4(),
        price: 100,
        name: `N95 NIOSH MASK WITH CE FDA 1`,
        des: desc,
        // pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}`,
        pix: `https://picsum.photos/${~~(Math.random() * 1000)}/?random`,
        quantity:1,
    },
    {
        id: uuidv4(),
        price: 200,
        name: `N95 NIOSH MASK WITH CE FDA 2`,
        des: desc,
        // pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}`,
        pix: `https://picsum.photos/${~~(Math.random() * 1000)}/?random`,
        quantity:1,
    },
    {
        id: uuidv4(),
        price: 300,
        name: `N95 NIOSH MASK WITH CE FDA 3`,
        des: desc,
        // pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}`,
        pix: `https://picsum.photos/${~~(Math.random() * 1000)}/?random`,
        quantity:1,
    },
    {
        id: uuidv4(),
        price: 400,
        name: `N95 NIOSH MASK WITH CE FDA 4`,
        des: desc,
        // pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}`,
        pix: `https://picsum.photos/${~~(Math.random() * 1000)}/?random`,
        quantity:1,
    },
    {
        id: uuidv4(),
        price: 500,
        name: `N95 NIOSH MASK WITH CE FDA 5`,
        des: desc,
        // pix: `https://api.adorable.io/avatars/100/${~~(Math.random() * 100)}`,
        pix: `https://picsum.photos/${~~(Math.random() * 1000)}/?random`,
        quantity:1,
    },

])