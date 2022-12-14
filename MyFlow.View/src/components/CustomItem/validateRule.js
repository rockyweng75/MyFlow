import valid from "@/utils/validate.js"

export default (item) => {
    var rules = []
    var trigger = 'blur';
    //自訂元件只有支援change
    if(item.ItemType.length >= 3) trigger = 'change'

    if(item.ItemSize && item.ItemSize > 0){
        rules.push( { required: item.Required == 'Y', message: '請輸入' + item.ItemTitle, trigger: trigger })
        rules.push( { min: 0, max : item.ItemSize, message: item.ItemTitle + '欄位長度不可超過' + item.ItemSize, trigger: trigger })
    } else {
        if(item.Required == 'Y'){
            rules.push( { required: true, message: '請輸入' + item.ItemTitle, trigger: trigger })
        } 
    }

    if(item.ItemFormat){
        rules.push( { validator: valid[item.ItemFormat], message: item.ItemTitle + '輸入格式有誤' , trigger: trigger })

        if(item.ItemFormat == 'validNumber'){
            if(item.ItemMax > 0 && item.ItemMin > 0){
                rules.push({ 
                    validator: valid['validMinMaxValue'],
                    min: item.ItemMin, max: item.ItemMax, 
                    message: '輸入值介於'+ item.ItemMin+'至' + item.ItemMax + '之間' , trigger: trigger});
            } else if(item.ItemMax > 0){
                rules.push({ 
                    validator: valid['validMaxValue'],
                    max: item.ItemMax, message: '輸入值不可大於' + item.ItemMax , trigger: trigger});
            } else if(item.ItemMin > 0){
                rules.push({ validator: valid['validMinValue'],
                     min: item.ItemMin, message: '輸入值不可小於' + item.ItemMin , trigger: trigger});
            }
        }
    }
    return rules
}