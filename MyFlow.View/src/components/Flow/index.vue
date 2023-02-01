<template>
    <canvas id="canvas" :width="width" :height="height" ref="canvasRef">
    </canvas>
</template>

<script>
    import { ref, onMounted } from 'vue'
    import { FlowCanvas } from './flow'
    export default({
        props:['width', 'height'],
        setup(){
            const canvasRef= ref({})
            console.log(canvasRef)
            onMounted(async ()=>{

                const canvas = new FlowCanvas(canvasRef.value)

                const stages = [{id: 1, stageName: 'test'}, {id: 2,  stageName: 'test'}, {id: 3,  stageName: 'test'}]

                for(let i = 0; i < stages.length; i++){
                    let node = await canvas.createNode('Stage');
                    node.title = stages[i].stageName
                }

                canvas.print()
            })

            return {
                canvasRef,
            }
        }
    })
</script>

<style scoped>
    canvas{
        background-color: beige;
    }
</style>