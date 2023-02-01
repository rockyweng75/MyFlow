export class FlowCanvas {
    canvas : HTMLCanvasElement;
    ctx : CanvasRenderingContext2D | null;
    width : number;
    height : number;
    startX : number;
    startY : number;
    offsetX : number;
    offsetY : number;
    isDown = false;
    isDrop = false;
    dropItem : IItem | null = null;
    nodes :IItem[] = [] ;
    connectLines : ConnectLine[] = [];
    points = [];

    constructor(canvas){
        this.width = canvas.width;
        this.height = canvas.height;
        this.canvas = canvas;

        if (this.canvas.getContext) {
            this.ctx = this.canvas.getContext('2d');
            this.offsetX = this.canvas.offsetLeft;
            this.offsetY = this.canvas.offsetTop;

            this.canvas.onmousedown = e => this.handleMouseDown(e);
            this.canvas.onmousemove = e => this.handleMouseMove(e);
            this.canvas.onmouseup = e => this.handleMouseUp(e);
            this.canvas.onmouseout = e => this.handleMouseOut(e);
        } else {
            alert('error')
        }
    }

    handleMouseDown(e) {
        e.preventDefault();
        e.stopPropagation();
    
        // save the starting x/y of the rectangle
        this.startX = e.clientX - this.offsetX;
        this.startY = e.clientY - this.offsetY;
    
        // set a flag indicating the drag has begun
        this.isDown = true;
    }
    
    handleMouseUp(e) {
        e.preventDefault();
        e.stopPropagation();
    
        // the drag is over, clear the dragging flag
        this.isDown = false;
    }
    
    handleMouseOut(e) {
        e.preventDefault();
        e.stopPropagation();
    
        // the drag is over, clear the dragging flag
        this.isDown = false;
    }
    
    async handleMouseMove (e) {
        e.preventDefault();
        e.stopPropagation();
        // // if we're not dragging, just return
        // if (!this.isDown) {
        //     return;
        // }
    
        // get the current mouse position
        let mouseX = e.clientX - this.offsetX;
        let mouseY = e.clientY - this.offsetY;
        let isReload = false 
        if(this.nodes){
            this.nodes.forEach(async (item) =>{
                // 拖曳
                if( this.isDown && this.isDrop ){
                    isReload = true;
                } else if(this.isDown && item.isInside(this.startX, this.startY)){
                    this.dropItem = item 
                    this.isDrop = true;
                } else {
                    this.isDrop = false;
                }              
            })
        }

        if(isReload){
            if(!this.isOverSide(mouseX, mouseY, this.dropItem!.width, this.dropItem!.height))
            {
                this.dropItem!.move(mouseX, mouseY);
                await this.clear();
                await this.print();       
            }
        }
    }

    print(){
        return new Promise<void>((resolve, reject)=>{
            if(this.nodes) 
                this.nodes
                    .forEach(item=> item.print());

            if(this.connectLines) 
                this.connectLines
                    .forEach(item=> item.print());

            resolve();
        });
    }

    clear(){
        return new Promise<void>((resolve, reject)=>{
            this.ctx!.clearRect(0, 0, this.canvas.width, this.canvas.height);
            resolve();
        });
    }

    isOverSide(mouseX, mouseY, objWidth, objHeight){
        if(mouseX < 0 || mouseY < 0){
            return true;
        } else if(mouseX + objWidth > this.width){
            return true;
        }
        else if(mouseY + objHeight > this.height){
            return true;
        } else {
            return false;
        }
    }

    createNode(type: string){
        return new Promise((resolve, reject)=>{
            let x = 15;
            let y = 15;
            let width = 50;
            let height = 50;
            if(this.nodes){
                this.nodes.forEach(item =>{
                    if(item.isExists(x, y, width, height)){
                        y += item.height + 10;
                        if(!this.isOverSide(x, y, width, height)){
                            this.canvas.height += item.height + 10
                        }
                    }
                })
            }
            let id = Date.now().toFixed();
            let node;
            // if(type === 'Diamond')
            //     node = new Diamond(this.ctx, id, x, y, width, height)
            // else if(type === 'Trapezoidal')
            //     node = new Trapezoidal(this.ctx, id, x, y, width, height)
            // else if(type === 'Round')
            //     node = new Round(this.ctx, id, x, y, width, height)
            if(type === 'Stage')
                node = new Stage(this.ctx, id, x, y, this.width - (x * 2), height)
            // else 
            //     node = new Rect(this.ctx, id, x, y, width, height)

            this.nodes.push(node);
            resolve(node);
        })
    }

    createConnectLine(node1, node2){
        return new Promise((resolve, reject)=>{
            let connectLine = new ConnectLine(this.ctx, node1, node2, 1);
            this.connectLines.push(connectLine)
            resolve(connectLine)
        });
    }
}

interface IItem {

    ctx : CanvasRenderingContext2D | null;
    x: number;
    y: number;
    width : number;
    height : number;
    text: string | undefined;
    title: string | undefined;
    id: string | number;

    isInside (mouseX, mouseY)

    move (newX, newY)

    resize (newWidth, newHeight)

    print()

    isExists(x, y, width, height)
}

export abstract class Item implements IItem{
    ctx : CanvasRenderingContext2D | null;
    x: number;
    y: number;
    width : number;
    height : number;
    text: string | undefined;
    id: string | number;
    title: string | undefined;

    constructor(ctx, id, x, y, width, height){
        this.ctx = ctx;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.id = id;
    }

    move (newX, newY)
    {
        this.x = newX;
        this.y = newY;
    }

    resize (newWidth, newHeight)
    {
        this.width = newWidth;
        this.height = newHeight;
    }

    isExists(x, y, width, height){
        if( this.x <= x 
            && x <= (this.x + this.width) 
            && this.y <= y 
            && y <= (this.y + this.height)
            && this.x <= x + width
            && x + width <= (this.x + this.width) 
            && this.y <= y + height
            && y + height <= (this.y + this.height) 
                ) {
            return true;
        }
    }

    abstract isInside (mouseX, mouseY)

    abstract print()

}

export class Stage extends Item {
    isInside (mouseX, mouseY)
    {
        if( this.x <= mouseX && mouseX <= (this.x + this.width) 
            && this.y <= mouseY && mouseY <= (this.y + this.height)
                ) {
            return true;
        }
        else return false;
    }

    print(){

        this.ctx!.beginPath();
        this.ctx!.strokeRect(this.x, this.y, this.width, this.height);
        if(this.title){
            console.log('print title')

            this.ctx!.beginPath();
            this.ctx!.moveTo(this.x, this.y + 3);
            this.ctx!.lineTo(this.x + this.width, this.y + 3)
            this.ctx!.closePath();
            this.ctx!.beginPath();
            this.ctx!.fillStyle = "black";
            this.ctx!.font = "14px Arial";
            this.ctx!.textAlign = "center";
            this.ctx!.fillText(this.title, this.x + 14, this.y + 14)
        }
    }
}

export class ConnectLine{
    ctx : CanvasRenderingContext2D | null;
    node1 : IItem;
    node2 : IItem;
    width : number;
    constructor(ctx, node1, node2, width){
        this.ctx = ctx;
        this.node1 = node1;
        this.node2 = node2;
        this.width = width;
    }

    print(){
        this.ctx!.beginPath();
        let node1 = this.node1;
        let node2 = this.node2;
        let top = node1.y <= node2.y ? node1 : node2;
        let bottom = node1.y > node2.y ? node1 : node2;

        this.ctx!.moveTo(top.x + top.width / 2, top.y + top.height);
        this.ctx!.lineTo(bottom.x + bottom.width / 2, bottom.y);
        this.ctx!.stroke();
    }
}


// export class Rect implements IItem {
//     ctx : CanvasRenderingContext2D | null;
//     x: number;
//     y: number;
//     width : number;
//     height : number;
//     text: string;
//     id: string | number;
//     constructor(ctx, id, x, y, width, height){
//         this.ctx = ctx;
//         this.x = x;
//         this.y = y;
//         this.width = width;
//         this.height = height;
//         this.id = id;
//     }

//     isInside  (mouseX, mouseY)
//     {
//         if( this.x <= mouseX && mouseX <= (this.x + this.width) 
//             && this.y <= mouseY && mouseY <= (this.y + this.height)
//                 ) {
//             return true;
//         }
//         else return false;
//     }

//     move (newX, newY)
//     {
//         console.log('move')
//         this.x = newX;
//         this.y = newY;
//     }

//     resize (newWidth, newHeight)
//     {
//         console.log('resize')
//         this.width = newWidth;
//         this.height = newHeight;
//     }

//     print(){
//         this.ctx!.beginPath();
//         this.ctx!.strokeRect(this.x, this.y, this.width, this.height);
//         if(this.text){
//             this.ctx!.font = "14px Arial";
//             this.ctx!.textAlign = "center";
//             this.ctx!.fillText(this.text, this.x + 14, this.y + 14)
//         }
//     }

//     isExists(x, y, width, height){
//         if( this.x <= x 
//             && x <= (this.x + this.width) 
//             && this.y <= y 
//             && y <= (this.y + this.height)
//             && this.x <= x + width
//             && x + width <= (this.x + this.width) 
//             && this.y <= y + height
//             && y + height <= (this.y + this.height) 
//                 ) {
//             return true;
//         }
//     }
// }


// class Diamond implements IItem{

//     ctx : CanvasRenderingContext2D | null;
//     x: number;
//     y: number;
//     width : number;
//     height : number;
//     text: string;
//     id: string | number;

//     constructor(ctx, id, x, y, width, height){
//         this.ctx = ctx;
//         this.id = id;
//         this.x = x;
//         this.y = y;
//         this.width = width;
//         this.height = height;
//     }
//     print(){
//         this.ctx!.beginPath();
//         this.ctx!.moveTo(this.x + this.width / 2, this.y);
//         this.ctx!.lineTo(this.x, this.y + this.height / 2);
//         this.ctx!.lineTo(this.x + this.width / 2, this.y + this.height);
//         this.ctx!.lineTo(this.x + this.width, this.y + this.height / 2);
//         this.ctx!.lineTo(this.x + this.width / 2, this.y);
//         this.ctx?.stroke();

//         if(this.text){
//             this.ctx!.font = "14px Arial";
//             this.ctx!.textAlign = "center";
//             this.ctx!.fillText(this.text, this.x + this.width / 3, this.y + this.height/2)
//         }
//     }

//     isInside (mouseX, mouseY){
//         console.log(mouseX, mouseY, this.x, this.y)

//         if( this.x <= mouseX && mouseX <= (this.x + this.width) 
//             && this.y <= mouseY && mouseY <= (this.y + this.height)
//             )
//         {
            
//         } else {
//             return false;
//         }

//         if( mouseY < this.y + this.height / 2 
//             && mouseY - this.y >= 0
//             && mouseX >= this.x + this.width / 2 - (mouseY - this.y)
//             && mouseX <= this.x + this.width / 2 + (mouseY - this.y)
//         ) {
//             return true
//         } 

//         if( mouseY > this.y + this.height / 2 
//             && mouseY - this.y >= 0
//             && mouseX >= this.x + (mouseY - this.y - this.height / 2)
//             && mouseX <= this.x + this.width - (mouseY - this.y - this.height / 2)
//         ) {
//             return true
//         } 

//         if(mouseY === this.y + this.height / 2 
//             && mouseX >= this.x
//             && mouseX <= this.x + this.width)
//         {
//             return true
//         } 
//         return false
//     }

//     isExists(x, y, width, height){
//         if( this.x <= x 
//             && x <= (this.x + this.width) 
//             && this.y <= y 
//             && y <= (this.y + this.height)
//             && this.x <= x + width
//             && x + width <= (this.x + this.width) 
//             && this.y <= y + height
//             && y + height <= (this.y + this.height) 
//                 ) {
//             return true;
//         }
//     }

//     move (newX, newY)
//     {
//         console.log('move')
//         this.x = newX;
//         this.y = newY;
//     }

//     resize (newWidth, newHeight)
//     {
//         console.log('resize')
//         this.width = newWidth;
//         this.height = newHeight;
//     }
// }

// class Trapezoidal implements IItem {
//     ctx : CanvasRenderingContext2D | null;
//     x: number;
//     y: number;
//     width : number;
//     height : number;
//     text: string;
//     id: string | number;
//     triangleAngle: number;
//     triangleWidth: number;
//     constructor(ctx, id, x, y, width, height){
//         this.ctx = ctx;
//         this.id = id;
//         this.x = x;
//         this.y = y;
//         this.width = width;
//         this.height = height;
//         this.triangleAngle = 5;
//         let r = this.height / Math.sin(this.triangleAngle);
//         this.triangleWidth = r * Math.cos(this.triangleAngle) * -1;
//     }
//     print(){
//         this.ctx!.beginPath();
//         this.ctx!.moveTo(this.x + this.triangleWidth, this.y);
//         this.ctx!.lineTo(this.x, this.y + this.height);
//         this.ctx!.lineTo(this.x + this.width - this.triangleWidth, this.y + this.height);
//         this.ctx!.lineTo(this.x + this.width, this.y);
//         this.ctx!.lineTo(this.x + this.triangleWidth, this.y);
//         this.ctx?.stroke();

//         if(this.text){
//             this.ctx!.font = "14px Arial";
//             this.ctx!.textAlign = "center";
//             this.ctx!.fillText(this.text, this.x + this.width / 3, this.y + this.height/3)
//         }
//     }

//     isInside  (mouseX, mouseY){
//         if(this.y <= mouseY && mouseY <= (this.y + this.height))
//         {
//         } else {
//             return false;
//         }
//         let r = (this.height - (mouseY - this.y)) / Math.sin(this.triangleAngle);
//         let triangleWidth = r * Math.cos(this.triangleAngle) * -1;
//         if( mouseX >= this.x + triangleWidth 
//             && mouseX <= this.x + this.width - triangleWidth
//         ) {
//             return true
//         } 

//         return false
//     }

//     isExists(x, y, width, height){
//         if( this.x <= x 
//             && x <= (this.x + this.width) 
//             && this.y <= y 
//             && y <= (this.y + this.height)
//             && this.x <= x + width
//             && x + width <= (this.x + this.width) 
//             && this.y <= y + height
//             && y + height <= (this.y + this.height) 
//                 ) {
//             return true;
//         }
//     }

//     move (newX, newY)
//     {
//         console.log('move')
//         this.x = newX;
//         this.y = newY;
//     }

//     resize (newWidth, newHeight)
//     {
//         console.log('resize')
//         this.width = newWidth;
//         this.height = newHeight;
//     }
// }

// class Round implements IItem{
//     ctx : CanvasRenderingContext2D | null;
//     x: number;
//     y: number;
//     width : number;
//     height : number;
//     text: string;
//     id: string | number;
//     triangleAngle: number;
//     triangleWidth: number;
//     constructor(ctx, id, x, y, width, height){
//         this.ctx = ctx;
//         this.id = id;
//         this.x = x;
//         this.y = y;
//         this.width = width;
//         this.height = height;
//         this.triangleAngle = 5;
//         let r = this.height / Math.sin(this.triangleAngle);
//         this.triangleWidth = r * Math.cos(this.triangleAngle) * -1;
//     }
//     print(){

//         this.ctx!.beginPath();
//         this.ctx!.arc(this.x  + this.width / 2, this.y + this.height/ 2, this.width /2, 0, 2 * Math.PI);

//         this.ctx?.stroke();

//         if(this.text){
//             this.ctx!.font = "14px Arial";
//             this.ctx!.textAlign = "center";
//             this.ctx!.fillText(this.text, this.x + this.width / 3, this.y + this.height/3)
//         }
//     }

//     isInside  (mouseX, mouseY){

//         let h = this.x + this.width / 2;
//         let k = this.y + this.height / 2;
//         let r = this.width / 2;
//         if(
//             Math.pow(mouseX, 2) + Math.pow(mouseY, 2) - (2 * h * mouseX) - (2 * k * mouseY) + Math.pow(h, 2) + Math.pow(k, 2) - Math.pow(r, 2) <= 0
//             && Math.pow(-2 * h, 2) + Math.pow( -2 * k, 2) - 4 * ( Math.pow(h, 2) + Math.pow(k, 2) - Math.pow(r, 2)) > 0
//         ){
//             return true;
//         }
        
//         return false
//     }

//     isExists(x, y, width, height){
//         if( this.x <= x 
//             && x <= (this.x + this.width) 
//             && this.y <= y 
//             && y <= (this.y + this.height)
//             && this.x <= x + width
//             && x + width <= (this.x + this.width) 
//             && this.y <= y + height
//             && y + height <= (this.y + this.height) 
//                 ) {
//             return true;
//         }
//     }

//     move (newX, newY)
//     {
//         console.log('move')
//         this.x = newX;
//         this.y = newY;
//     }

//     resize (newWidth, newHeight)
//     {
//         console.log('resize')
//         this.width = newWidth;
//         this.height = newHeight;
//     }
// }