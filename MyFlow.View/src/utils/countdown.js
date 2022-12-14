export const getTimeDesc = (totalMinutes) => {
    if(totalMinutes / 60 / 24 > 0){
        return Math.floor(totalMinutes / 60 / 24) + '天'
         + Math.floor(totalMinutes % (60 * 24) / 60) + '小時'
    } else if(totalMinutes / 60 > 0){
        return Math.floor(totalMinutes / 60) + '小時';
    } else {
        return totalMinutes + '分鐘';
    }
}

export const determineType = (totalMinutes) =>{
    var day = Math.floor(totalMinutes / 60 / 24)
    if(day > 2){
        return 'info'
    } 

    if(day <= 2 && day > 0){
        return 'warning'
    }

    if(day <= 0){
        return 'danger'
    }
}