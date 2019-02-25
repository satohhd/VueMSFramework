<template>
    <label  :id="_id" :class="field.name">
        &nbsp;&nbsp;&nbsp;&nbsp;<span v-html="field.caption" />&nbsp;:&nbsp;<span style="font-weight:bolder;" v-html="viewModel[field.name]"></span>
    </label>
</template>

<script>
    /* ******************************************************************
    *
    * ae-tag-xxxxxxxx
    *
    * in  string    page
    * in  string    section
    * in  {}        field
    * in  {}        viewModel
    * in  {}        state
    *
    ******************************************************************* */
    import aeTag from "./ae-tag.js"
    export default {
        mixins: [aeTag],
        props: [
        ],
        data() {
            return {
            }
        },
        watch: {

        },
        methods: {

        },
 
    }

</script>
<style scoped>
</style>
