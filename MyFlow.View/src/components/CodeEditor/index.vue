<template>
    <Codemirror
      v-model:value="modelValue"
      :options="cmOptions"
      border
      placeholder="test placeholder"
      :height="200"
      v-on:change="change"
    />
  </template>
  
  <script>
  
  import Codemirror from "codemirror-editor-vue3";
  
  // language
  import "codemirror/mode/javascript/javascript.js";
  
  // theme
  import "codemirror/theme/base16-dark.css";
  
  import _CodeMirror from "codemirror";
  // _CodeMirror.lineAtHeight(100);
  
  import { reactive, toRefs } from "vue";
  export default {
    components: { Codemirror },
    props:['modelValue', 'readonly'],
    props:{
      modelValue: { type: [Object,String] },
      readonly: { type: Boolean },
    }, 
    emits:['update:modelValue'],
    setup(props, {emit}) {
        
      const state = reactive({
      })
  
      const change = (val) =>{
          emit('update:modelValue', val)
      }
  
      return {
          ...toRefs(state),
          cmOptions: {
              mode: "text/javascript", // Language mode
              theme: "base16-dark", // Theme
              lineNumbers: true, // Show line number
              smartIndent: true, // Smart indent
              indentUnit: 2, // The smart indent unit is 2 spaces in length
              foldGutter: true, // Code folding
              styleActiveLine: true, // Display the style of the selected row
              readOnly: props.readonly ? true : false
          },
          change
      };
    },
  };
  </script>
  <style>
      .codemirror-container{
          width: 100%;
          display: grid;
          line-height: 1.5em;
          font-size: 1.2em;
      }
  </style>